import { useState } from "react";

import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import Paper from "@mui/material/Paper";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import CircularProgress from "@mui/material/CircularProgress";

import styles from "./home.module.css";
import homeBanner from "../../assets/homeBanner.jpg";
import logo from "../../assets/logo.png";

enum formType {
  Login,
  Register,
}

interface formDataProps {
  name: string;
  email: string;
  password: string;
}

const defaultData: formDataProps = {
  name: "",
  email: "",
  password: "",
};

export const HomePage = () => {
  const [formStatus, updateFormStatus] = useState<formType>(0);
  const [formData, updateFormData] = useState<formDataProps>(defaultData);
  const [isLoadingRequest, updateLoadingRequestStatus] =
    useState<boolean>(false);

  // FUNCTIONS------
  const onLogin = () => {
    const { email, password } = formData;
    console.log("Login");
  };

  const onRegister = () => {
    const { name, email, password } = formData;
    console.log("Register");
  };

  const updateInput = (e: any, key: string) => {
    const value: string = e.target.value;
    updateFormData({ ...formData, [key]: value });
  };

  const submittedForm = () => {
    const { name, email, password } = formData;

    switch (formStatus) {
      case formType.Login:
        if (!email.trim() || !password.trim()) {
          return alert("Por favor completa los campos restantes");
        }

        onLogin();
        break;

      case formType.Register:
        if (!name.trim() || !email.trim() || !password.trim()) {
          return alert("Por favor completa los campos restantes");
        }

        onRegister();
        break;

      default:
        break;
    }
  };

  const SwitchFormType = () => {
    updateFormData(defaultData);
    updateFormStatus(
      formStatus === formType.Login ? formType.Register : formType.Login
    );
  };

  const TextFieldStyling = { width: "100%", marginBottom: 2 };

  return (
    <Paper
      elevation={5}
      sx={{
        marginTop: { xs: 4, md: 10 },
        maxWidth: 800,
        marginX: "auto",
        borderRadius: 4,
      }}
    >
      <Grid
        container
        sx={{ height: "100%" }}
        justifyContent="space-between"
        alignItems="stretch"
      >
        <Grid
          item
          xs={0}
          md={5}
          className={styles.leftSideContainer}
          sx={{
            display: { xs: "none", md: "flex" },
            borderRadius: 4,
            overflow: "hidden",
          }}
        >
          <img src={homeBanner} alt="foto" />
        </Grid>

        <Grid
          container
          item
          xs={12}
          md={7}
          direction="column"
          justifyContent="center"
          alignItems="center"
          sx={{ padding: 2, paddingY: 4 }}
          component="form"
          onSubmit={submittedForm}
        >
          <img src={logo} alt="foto" style={{ width: 150 }} />

          <Typography variant="h5" fontWeight={700} gutterBottom>
            {formStatus === formType.Login
              ? "¡Inicia sesión!"
              : "¡Registrar cuenta!"}
          </Typography>

          {formStatus === formType.Register && (
            <TextField
              required
              value={formData.name}
              placeholder="Ingresa tu nombre completo"
              label="Nombre completo"
              sx={{ ...TextFieldStyling }}
              onChange={(e) => {
                updateInput(e, "name");
              }}
            />
          )}

          <TextField
            required
            value={formData.email}
            type="email"
            placeholder="Ingresa tu correo electrónico"
            label="Correo electrónico"
            sx={{ ...TextFieldStyling }}
            onChange={(e) => {
              updateInput(e, "email");
            }}
          />

          <TextField
            required
            value={formData.password}
            type="password"
            placeholder="Ingresa tu contraseña o clave"
            label="Contraseña"
            sx={{ ...TextFieldStyling }}
            onChange={(e) => {
              updateInput(e, "password");
            }}
          />

          <Button
            variant="contained"
            color="primary"
            fullWidth
            startIcon={
              isLoadingRequest && (
                <CircularProgress size={20} sx={{ color: "white" }} />
              )
            }
            disabled={isLoadingRequest}
            onClick={submittedForm}
          >
            {formStatus === formType.Login
              ? "Iniciar sesión"
              : "Registrarse ahora"}
          </Button>

          <Typography sx={{ marginTop: 3 }}>
            {formStatus === formType.Login
              ? "¿Eres nuevo?"
              : "¿Ya tienes cuenta?"}
          </Typography>

          <Button
            color="primary"
            fullWidth
            disabled={isLoadingRequest}
            onClick={SwitchFormType}
          >
            {formStatus === formType.Login
              ? "Registrar cuenta"
              : "Iniciar sesión"}
          </Button>
        </Grid>
      </Grid>
    </Paper>
  );
};
