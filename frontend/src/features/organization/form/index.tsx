import { useState, useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { axiosInstance } from "../../../app/config";

import Grid from "@mui/material/Grid";
import Divider from "@mui/material/Divider";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";

export const OrganizationFormPage = () => {
  const navigate = useNavigate();
  let [searchParams] = useSearchParams();

  const [isEditingActivity, updateIsEditing] = useState<boolean>(false);
  const [organizationData, updateOrganizationData] = useState({
    id: 0,
    accountId: 0,
    name: "",
    alias: "",
  });

  const formStyling = { marginBottom: 2, padding: 1 };

  const checkIsEditing = () => {
    const id = searchParams.get("edit");
    const accountId = searchParams.get("acc");

    if (id && accountId) {
      updateIsEditing(true);
      axiosInstance
        .get(`/Organization/${id}`)
        .then((response) => {
          const { data } = response;
          updateOrganizationData({
            ...organizationData,
            id: data.id,
            accountId: data.accountId,
            name: data.name,
            alias: data.alias,
          });
        })
        .catch((error) => {
          navigate("/dashboard");
        });
    }
  };

  const onChangeInput = (key: string, value: string | number) => {
    updateOrganizationData({
      ...organizationData,
      [key]: value,
    });
  };

  useEffect(() => {
    checkIsEditing();
  }, []);

  const submitForm = () => {
    const { name, alias } = organizationData;

    if (!name || !alias) {
      alert("Completa los campos restantes!");
      return false;
    }

    const payload = {
      name,
      alias,
    };

    if (isEditingActivity) {
      // UPDATE ACTIVITY
      axiosInstance
        .put(`/Organization/${organizationData.id}`, payload)
        .then((response) => {
          alert("¡Cambios realizado exitosamente!");
          navigate("/organization");
        })
        .catch((error) => {
          alert("Error por parte del servidor");
        });
    } else {
      // CREATE ACTIVITY
      axiosInstance
        .post(`/Organization`, payload)
        .then((response) => {
          alert("Organización creada exitosamente!");
          navigate("/dashboard");
        })
        .catch((error) => {
          alert("Error por parte del servidor");
        });
    }
  };

  return (
    <Grid container>
      <Typography gutterBottom variant="h5" fontWeight={500}>
        Organización
      </Typography>

      <Grid
        container
        item
        direction="row"
        justifyContent="space-between"
        alignItems="flex-start"
        sx={{
          background: "#ffffff",
          boxShadow: 9,
          borderRadius: 2,
          padding: 1,
        }}
      >
        <Grid item xs={12} md={6} sx={{ ...formStyling }}>
          <TextField
            label="Nombre completo"
            placeholder="Ingresa el nombre completo de la entidad"
            fullWidth
            value={organizationData.name}
            onChange={(e) => {
              onChangeInput("name", e.target.value);
            }}
          />
        </Grid>

        <Grid item xs={12} md={6} sx={{ ...formStyling }}>
          <TextField
            label="Alias"
            placeholder="Ingresa el alias o nombre corto del negocio"
            fullWidth
            value={organizationData.alias}
            onChange={(e) => {
              onChangeInput("alias", e.target.value);
            }}
          />
        </Grid>

        <Divider sx={{ width: "100%" }} />

        <Grid item container sx={{ marginY: 2 }}>
          <Button
            fullWidth
            variant="contained"
            color="primary"
            onClick={submitForm}
          >
            {isEditingActivity ? "Guardar cambios" : "Crear organización>"}
          </Button>
        </Grid>
      </Grid>
    </Grid>
  );
};
