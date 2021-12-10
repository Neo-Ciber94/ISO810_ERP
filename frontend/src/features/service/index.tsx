import { useState, useEffect } from "react";
import { axiosInstance } from "../../app/config";

import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import CircularProgress from "@mui/material/CircularProgress";
import ServiceCard from "../../app/components/cards/ServiceCard";

import * as Models from "../../models/index";

interface DashboardDataProps {
  Services: Models.Service[];
}

export const ServicePage = () => {
  const [serviceData, updateServiceData] = useState<DashboardDataProps>({
    Services: [],
  });

  const [isLoadingServices, updateLoadingData] = useState<boolean>(true);

  const GetServices = () => {
    axiosInstance.get("/Service").then((response) => {
      const { data } = response;

      updateLoadingData(false);
      updateServiceData({
        ...serviceData,
        Services: data,
      });
    });
  };

  useEffect(() => {
    GetServices();
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
          Servicios predeterminados
        </Typography>
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
        {isLoadingServices && (
          <Grid container item justifyContent="center" alignItems="center">
            <CircularProgress />
          </Grid>
        )}

        {serviceData.Services.length
          ? serviceData.Services.map((service) => (
              <ServiceCard key={service.id} {...service} />
            ))
          : null}
      </Grid>
    </Grid>
  );
};
