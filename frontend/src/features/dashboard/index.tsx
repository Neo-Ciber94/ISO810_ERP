import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { axiosInstance } from "../../app/config";

import Grid from "@mui/material/Grid";
import Divider from "@mui/material/Divider";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import CircularProgress from "@mui/material/CircularProgress";
import ActivityCard from "../../app/components/cards/ActivityCard";

import * as Models from "../../models/index";

interface DashboardDataProps {
  Organizations: Models.Organization[];
  Expenses: Models.Expense[];
}

export const DashboardPage = () => {
  const navigate = useNavigate();
  const [selectedOrganization, updateSelectedOrganization] =
    useState<number>(0);

  const [activityData, updateActivityData] = useState<DashboardDataProps>({
    Organizations: [],
    Expenses: [],
  });

  const [isLoadingExpenses, updateLoadingData] = useState<boolean>(false);

  const GetOrganizations = () => {
    axiosInstance.get("/Organization").then((response) => {
      const { data } = response;

      updateActivityData({
        ...activityData,
        Organizations: data,
        Expenses: [],
      });
    });
  };

  const onChangeOrg = (value: any) => {
    updateSelectedOrganization(parseInt(value));
    updateActivityData({
      ...activityData,
      Expenses: [],
    });
  };

  const GetExpenses = () => {
    axiosInstance
      .get("/Expense/" + selectedOrganization)
      .then((response) => {
        const { data } = response;
        updateLoadingData(false);
        updateActivityData({
          ...activityData,
          Expenses: data,
        });
      })
      .catch(() => {
        updateLoadingData(false);
      });
  };

  useEffect(() => {
    GetOrganizations();
  }, []);

  useEffect(() => {
    if (selectedOrganization) {
      updateLoadingData(true);
      GetExpenses();
    }
  }, [selectedOrganization]);

  return (
    <Grid container>
      <Grid
        container
        item
        direction="row"
        alignItems="center"
        justifyContent="space-between"
        sx={{ marginBottom: 2 }}
      >
        <Typography gutterBottom variant="h5" fontWeight={500}>
          Actividad
        </Typography>

        <Button
          variant="contained"
          onClick={() => {
            navigate("/activity");
          }}
        >
          Agregar actividad
        </Button>
      </Grid>

      <Grid
        container
        direction="column"
        justifyContent="flex-start"
        alignItems="flex-start"
        sx={{
          background: "#ffffff",
          boxShadow: 9,
          borderRadius: 2,
          padding: 1,
        }}
      >
        <Grid item container sx={{ marginY: 2 }}>
          <FormControl fullWidth>
            <InputLabel id="demo-simple-select-label">Organización</InputLabel>
            <Select
              value={selectedOrganization}
              label="Organización"
              onChange={(e) => {
                onChangeOrg(e.target.value);
              }}
            >
              <MenuItem value={0}>Selecciona una organización</MenuItem>
              {activityData.Organizations.map((Organization) => {
                const { id, alias } = Organization;
                return (
                  <MenuItem key={id} value={id}>
                    {alias}
                  </MenuItem>
                );
              })}
            </Select>
          </FormControl>
        </Grid>
        <Divider sx={{ marginBottom: 2, width: "100%" }} />

        {isLoadingExpenses && (
          <Grid container item justifyContent="center" alignItems="center">
            <CircularProgress />
          </Grid>
        )}

        {activityData.Expenses.length ? (
          activityData.Expenses.map((expense) => (
            <ActivityCard key={expense.id} {...expense} />
          ))
        ) : (
          <Typography
            variant="h6"
            color="textSecondary"
            align="center"
            sx={{ width: "100%" }}
            gutterBottom
          >
            {selectedOrganization
              ? "No hay actividad disponible"
              : "Debes seleccionar una organización"}
          </Typography>
        )}
      </Grid>
    </Grid>
  );
};
