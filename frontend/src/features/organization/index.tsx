import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { axiosInstance } from "../../app/config";

import Grid from "@mui/material/Grid";

import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import CircularProgress from "@mui/material/CircularProgress";
import OrganizationCard from "../../app/components/cards/OrganizationCard";

import * as Models from "../../models/index";

interface DashboardDataProps {
  Organizations: Models.Organization[];
}

export const OrganizationPage = () => {
  const navigate = useNavigate();

  const [activityData, updateActivityData] = useState<DashboardDataProps>({
    Organizations: [],
  });

  const [isLoadingOrganizations, updateLoadingData] = useState<boolean>(true);

  const GetOrganizations = () => {
    axiosInstance.get("/Organization").then((response) => {
      const { data } = response;

      updateLoadingData(false);
      updateActivityData({
        ...activityData,
        Organizations: data,
      });
    });
  };

  const onChangeOrg = (value: any) => {
    updateActivityData({
      ...activityData,
    });
  };

  useEffect(() => {
    GetOrganizations();
  }, []);

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
          Organización
        </Typography>

        <Button
          variant="contained"
          onClick={() => {
            navigate("form");
          }}
        >
          Agregar organización
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
          paddingTop: 3,
        }}
      >
        {isLoadingOrganizations && (
          <Grid container item justifyContent="center" alignItems="center">
            <CircularProgress />
          </Grid>
        )}

        {activityData.Organizations.length
          ? activityData.Organizations.map((organization) => (
              <OrganizationCard key={organization.id} {...organization} />
            ))
          : null}

        {!activityData.Organizations.length &&
        isLoadingOrganizations === false ? (
          <Typography
            variant="h6"
            color="textSecondary"
            align="center"
            sx={{ width: "100%", marginBottom: 2 }}
            gutterBottom
          >
            No hay organizaciones disponibles, debes crear uno
          </Typography>
        ) : null}
      </Grid>
    </Grid>
  );
};
