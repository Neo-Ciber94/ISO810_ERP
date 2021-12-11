import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { axiosInstance } from "../../app/config";
import { useAppContext } from "../../app/hooks/useAppContext";

import Grid from "@mui/material/Grid";
import Divider from "@mui/material/Divider";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";

export const OptionsPage = () => {
  const navigate = useNavigate();
  const { updateUserData } = useAppContext();
  const [userData, updateFormData] = useState({
    id: 0,
    name: "",
    email: "",
    password: "",
  });

  const formStyling = { marginBottom: 2, padding: 1 };

  const GetUserData = () => {
    axiosInstance.get("/Account/me").then((response) => {
      const { data } = response;
      updateFormData({
        ...userData,
        id: data.id,
        email: data.email,
        name: data.name,
      });
    });
  };

  const onChangeInput = (key: string, value: string | number) => {
    updateFormData({
      ...userData,
      [key]: value,
    });
  };

  useEffect(() => {
    GetUserData();
  }, []);

  const submitForm = () => {
    const { id, name, email, password } = userData;
    if (!id || !name || !email || !password) {
      alert("Completa los campos restantes!");
      return false;
    }

    const payload = {
      name,
      email,
      password,
    };

    axiosInstance
      .put(`/Account/update`, payload)
      .then((response) => {
        const { data } = response;
        console.log(data);
        if (!data.success) {
          alert("Error: Utiliza otro email");
          return false;
        }

        alert("¡Cambios realizado exitosamente!");
      })
      .catch((error) => {
        alert("Error por parte del servidor");
      });
  };

  const LogOut = () => {
    updateUserData({ user: { isAuthenticated: false } });
    navigate("/");
  };

  const DeleteAccount = () => {
    if (
      window.confirm(
        "¿Estas seguro que deseas borrar esta cuenta? \n¡Esta actividad sera irreversible!"
      )
    ) {
      axiosInstance.delete("/Account/delete").then(() => {
        alert(
          "¡Usuario eliminado exitosamente! \nSiempre te estaremos esperando ❤️"
        );

        LogOut();
      });
    }
  };

  return (
    <Grid container>
      <Typography gutterBottom variant="h5" fontWeight={500}>
        Opciones del usuario
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
          <TextField label="ID" disabled fullWidth value={userData.id} />
        </Grid>

        <Grid item xs={12} md={6} sx={{ ...formStyling }}>
          <TextField
            label="Nombre"
            placeholder="Ingrese su nombre completo"
            fullWidth
            value={userData.name}
            onChange={(e) => {
              onChangeInput("name", e.target.value);
            }}
          />
        </Grid>

        <Grid item xs={12} md={6} sx={{ ...formStyling }}>
          <TextField
            label="Correo electrónico"
            type="email"
            placeholder="Ingrese su correo electrónico"
            fullWidth
            value={userData.email}
            onChange={(e) => {
              onChangeInput("email", e.target.value);
            }}
          />
        </Grid>

        <Grid item xs={12} md={6} sx={{ ...formStyling }}>
          <TextField
            label="Contraseña"
            type="password"
            placeholder="Ingrese la clave"
            fullWidth
            value={userData.password}
            onChange={(e) => {
              onChangeInput("password", e.target.value);
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
            Guardar cambios
          </Button>
        </Grid>

        <Divider sx={{ width: "100%" }} />
        <Typography
          sx={{ width: "100%", marginTop: 2, marginBottom: 2 }}
          align="center"
          gutterBottom
        >
          Si borras tu cuenta, esta actividad sera irreversible
        </Typography>

        <Button
          fullWidth
          variant="outlined"
          color="error"
          onClick={DeleteAccount}
          startIcon={<DeleteForeverIcon />}
        >
          Borrar Cuenta
        </Button>
      </Grid>
    </Grid>
  );
};
