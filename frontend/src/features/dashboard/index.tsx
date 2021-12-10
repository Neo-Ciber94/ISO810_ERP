import { useState, useEffect } from "react";

import Grid from "@mui/material/Grid";
import Divider from "@mui/material/Divider";
import Typography from "@mui/material/Typography";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";

import * as Models from "../../models/index";

interface DashboardDataProps {
  Organizations: Models.Organization[];
  Expenses: Models.Expense[];
}

export const DashboardPage = () => {
  const [selectedOrganization, updateSelectedOrganization] =
    useState<number>(0);

  const [activityData, updateActivityData] = useState<DashboardDataProps>({
    Organizations: [],
    Expenses: [],
  });

  const [loadingData, updateLoadingData] = useState({
    isLoadingOrganizations: true,
    isLoadingExpenses: true,
  });

  useEffect(() => {}, []);

  const GetOrganizations = () => {};

  const GetExpenses = () => {};

  return (
    <Grid container>
      <Typography gutterBottom variant="h5" fontWeight={500}>
        Actividad
      </Typography>

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
          <FormControl fullWidth disabled={loadingData.isLoadingOrganizations}>
            <InputLabel id="demo-simple-select-label">Organización</InputLabel>
            <Select
              labelId="demo-simple-select-label"
              id="demo-simple-select"
              value={10}
              label="Organización"
            >
              <MenuItem value={10}>Ten</MenuItem>
              <MenuItem value={20}>Twenty</MenuItem>
              <MenuItem value={30}>Thirty</MenuItem>
            </Select>
          </FormControl>
        </Grid>
        <Divider sx={{ marginBottom: 2, width: "100%" }} />
        asdad
      </Grid>
    </Grid>
  );
};
