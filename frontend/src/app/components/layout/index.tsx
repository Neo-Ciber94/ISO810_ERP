import { useState } from "react";

import Grid from "@mui/material/Grid";
import IconButton from "@mui/material/IconButton";
import SideBarContent from "./components/SidebarContent";
import MenuIcon from "@mui/icons-material/Menu";
import Sidebar from "./components/Sidebar";
import Logo from "../../../assets/logo.png";

const Layout = ({ children }: { children: JSX.Element }) => {
  const [isOpenSidebar, updateSidebarStatus] = useState<boolean>(false);
  const openSideBar = () => {
    updateSidebarStatus(true);
  };
  const closeSideBar = () => {
    updateSidebarStatus(false);
  };

  return (
    <Grid
      container
      direction="column"
      justifyContent="flex-start"
      alignItems="flex-start"
    >
      <Sidebar isOpen={isOpenSidebar} onOclose={closeSideBar} />

      <Grid
        container
        item
        sx={{
          display: { xs: "flex", md: "none" },
          background: "#ffffff",
          boxShadow: 9,
          paddingX: 1,
        }}
        direction="row"
        alignItems="center"
        justifyContent="space-between"
      >
        <Grid item>
          <img src={Logo} alt="Hypercode logo" width={150} />
        </Grid>

        <Grid item>
          <IconButton size="large" color="primary" onClick={openSideBar}>
            <MenuIcon />
          </IconButton>
        </Grid>
      </Grid>

      <Grid
        container
        direction="row"
        alignItems="flex-start"
        justifyContent="flex-start"
      >
        <Grid
          container
          item
          xs={0}
          md={2}
          direction="column"
          justifyContent="flex-start"
          alignItems="center"
          sx={{
            display: { xs: "none", md: "flex" },
            background: "#ffffff",
            boxShadow: 9,
            minHeight: "100vh",
            padding: 2,
            borderRadius: "0 1rem 1rem 0",
          }}
        >
          <Grid item>
            <img
              src={Logo}
              alt="Hypercode logo"
              style={{ width: 200, marginBottom: 2 }}
            />
          </Grid>

          <SideBarContent />
        </Grid>

        <Grid
          container
          justifyContent="flex-start"
          alignItems="flex-start"
          item
          xs={12}
          md={7}
          sx={{ marginTop: 4, padding: 2 }}
        >
          {children}
        </Grid>
      </Grid>
    </Grid>
  );
};

export default Layout;
