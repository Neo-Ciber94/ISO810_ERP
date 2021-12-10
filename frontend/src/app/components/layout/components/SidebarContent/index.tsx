import { useNavigate } from "react-router-dom";
import { axiosInstance } from "../../../../config";
import { useAppContext } from "../../../../hooks/useAppContext";

import Grid from "@mui/material/Grid";
import Button from "@mui/material/Button";
import HomeIcon from "@mui/icons-material/Home";
import BusinessCenterIcon from "@mui/icons-material/BusinessCenter";
import DesignServicesIcon from "@mui/icons-material/DesignServices";
import LogoutIcon from "@mui/icons-material/Logout";

interface SidebarContentProps {
  externalFunction?: any;
}

const SidebarContent = ({ externalFunction }: SidebarContentProps) => {
  const { updateUserData } = useAppContext();
  const navigate = useNavigate();

  const redirectUser = (url: string) => {
    if (externalFunction) {
      externalFunction();
    }

    navigate(url);
  };

  const logOutUser = () => {
    if (externalFunction) {
      externalFunction();
    }

    axiosInstance.post("/Account/logout").then(() => {
      updateUserData({ user: { isAuthenticated: false } });
    });
  };

  return (
    <Grid container direction="column" sx={{ color: "rgba(0,0,0,0.6)" }}>
      <NavLink
        label="Inicio"
        icon={<HomeIcon />}
        onClick={() => {
          redirectUser("/dashboard");
        }}
      />

      <NavLink
        label="Organizaciones"
        icon={<BusinessCenterIcon />}
        onClick={() => {
          redirectUser("/organization");
        }}
      />

      <NavLink
        label="Servicios"
        icon={<DesignServicesIcon />}
        onClick={() => {
          redirectUser("/service");
        }}
      />

      <NavLink
        label="Cerrar sesión"
        icon={<LogoutIcon />}
        onClick={logOutUser}
      />
    </Grid>
  );
};

interface NavLinkProps {
  label: string;
  icon: any;
  onClick?: any;
}

const NavLink = ({ label, icon, onClick }: NavLinkProps) => (
  <Button
    color="inherit"
    fullWidth
    startIcon={icon}
    onClick={onClick}
    variant="outlined"
    sx={{
      padding: 2,
      marginBottom: 2,
      borderRadius: 2,
      textTransform: "none",
      justifyContent: "flex-start",
    }}
  >
    {label}
  </Button>
);

export default SidebarContent;
