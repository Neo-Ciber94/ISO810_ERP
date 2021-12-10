import Grid from "@mui/material/Grid";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import LinkIcon from "@mui/icons-material/Link";

import * as Models from "../../../../models";

const ServiceCard = ({ name, homePage }: Models.Service) => {
  return (
    <Grid
      container
      direction="row"
      alignItems="center"
      justifyContent="space-between"
      sx={{
        border: 1,
        borderColor: "divider",
        marginBottom: 2,
        borderRadius: 2,
        padding: 2,
      }}
    >
      <Grid container item xs={6} alignItems="center">
        <Avatar sx={{ bgcolor: "#4caf50" }} variant="rounded">
          {name.slice(0, 1)}
        </Avatar>

        <Grid sx={{ marginLeft: 1, display: "flex", flexDirection: "column" }}>
          <Typography fontWeight={500}>{name}</Typography>
          <Typography color="textSecondary" sx={{ fontSize: 12 }}>
            {homePage}
          </Typography>
        </Grid>
      </Grid>

      <Grid container item xs={6} alignItems="center" justifyContent="flex-end">
        <Button
          startIcon={<LinkIcon />}
          variant="contained"
          onClick={() => {
            window.open(homePage);
          }}
        >
          Visitar sitio web
        </Button>
      </Grid>
    </Grid>
  );
};

export default ServiceCard;
