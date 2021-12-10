import { useState, useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { axiosInstance } from "../../app/config";
import { currencies, Services } from "../../app/dummyData";

import Grid from "@mui/material/Grid";
import Divider from "@mui/material/Divider";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";

export const ActivityPage = () => {
  const navigate = useNavigate();
  let [searchParams] = useSearchParams();

  const [isEditingActivity, updateIsEditing] = useState<boolean>(false);
  const [activityData, updateActivityData] = useState({
    id: 0,
    organizationId: 0,
    service: 0,
    currency: 0,
    amount: "",
  });

  const [formOptions, updateFormOptions] = useState({
    organizations: [],
    services: Services,
    currencies: currencies,
  });

  const formStyling = { marginBottom: 2, padding: 1 };

  const GetOrganizations = () => {
    axiosInstance.get("/Organization").then((response) => {
      const { data } = response;

      updateFormOptions({ ...formOptions, organizations: data });
    });
  };

  const checkIsEditing = () => {
    const id = searchParams.get("edit");
    const organizationId = searchParams.get("org");

    if (id && organizationId) {
      updateIsEditing(true);
      axiosInstance
        .get(`/Expense/${organizationId}/${id}`)
        .then((response) => {
          const { data } = response;
          updateActivityData({
            ...activityData,
            id: data.id,
            organizationId: data.organizationId,
            service: data.serviceId,
            currency: data.currencyId,
            amount: data.amount,
          });
        })
        .catch((error) => {
          navigate("/dashboard");
        });
    }
  };

  const onChangeInput = (key: string, value: string | number) => {
    updateActivityData({
      ...activityData,
      [key]: value,
    });
  };

  useEffect(() => {
    GetOrganizations();
    checkIsEditing();
  }, []);

  const submitForm = () => {
    const { organizationId, service, currency, amount } = activityData;

    if (!organizationId || !service || !currency || !amount) {
      alert("Completa los campos restantes!");
      return false;
    }

    const payload = {
      organizationId,
      serviceId: service,
      currencyId: currency,
      amount,
    };

    if (isEditingActivity) {
      // UPDATE ACTIVITY
      axiosInstance
        .put(`/Expense/${organizationId}/${activityData.id}`, payload)
        .then((response) => {
          alert("¡Cambios realizado exitosamente!");
        })
        .catch((error) => {
          alert("Error por parte del servidor");
        });
    } else {
      // CREATE ACTIVITY
      axiosInstance
        .post(`/Expense`, payload)
        .then((response) => {
          alert("¡Actividad creada exitosamente!");
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
        Actividad
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
          <FormControl fullWidth>
            <InputLabel>Organización</InputLabel>
            <Select
              value={activityData.organizationId}
              label="Organización"
              disabled={isEditingActivity}
              onChange={(e) => {
                onChangeInput("organizationId", e.target.value);
              }}
            >
              <MenuItem value={0}>Selecciona una organización</MenuItem>
              {formOptions.organizations.map((Organization) => {
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

        <Grid item xs={12} md={6} sx={{ ...formStyling }}>
          <FormControl fullWidth>
            <InputLabel>Servicio</InputLabel>
            <Select
              value={activityData.service}
              label="Servicio"
              onChange={(e) => {
                onChangeInput("service", e.target.value);
              }}
            >
              <MenuItem value={0}>
                Selecciona la empresa que ofrece el servicio
              </MenuItem>
              {formOptions.services.map((service) => {
                const { id, name } = service;
                if (!id) return null;
                return (
                  <MenuItem key={id} value={id}>
                    {name}
                  </MenuItem>
                );
              })}
            </Select>
          </FormControl>
        </Grid>

        <Grid item xs={12} md={6} sx={{ ...formStyling }}>
          <FormControl fullWidth>
            <InputLabel>Divisa</InputLabel>
            <Select
              value={activityData.currency}
              label="Divisa"
              onChange={(e) => {
                onChangeInput("currency", e.target.value);
              }}
            >
              <MenuItem value={0}>Selecciona la divisa</MenuItem>
              {formOptions.currencies.map((currency) => {
                const { id, name } = currency;
                return (
                  <MenuItem key={id} value={id}>
                    {name}
                  </MenuItem>
                );
              })}
            </Select>
          </FormControl>
        </Grid>

        <Grid item xs={12} md={6} sx={{ ...formStyling }}>
          <TextField
            label="Monto"
            placeholder="Ingresa el monto total del gasto"
            type="number"
            fullWidth
            value={activityData.amount}
            onChange={(e) => {
              onChangeInput("amount", e.target.value);
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
            {isEditingActivity ? "Guardar cambios" : "Crear >"}
          </Button>
        </Grid>
      </Grid>
    </Grid>
  );
};
