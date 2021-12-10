import { useNavigate } from "react-router-dom";
import { Services, currencies } from "../../../dummyData";
import { axiosInstance } from "../../../config";

import Grid from "@mui/material/Grid";
import Avatar from "@mui/material/Avatar";
import Divider from "@mui/material/Divider";
import Typography from "@mui/material/Typography";
import IconButton from "@mui/material/IconButton";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";

import * as Models from "../../../../models";

const ActivityCard = ({
  id,
  organizationId,
  serviceId,
  currencyId,
  amount,
  createdAt,
}: Models.Expense) => {
  const navigate = useNavigate();
  const date: any = new Date(`${createdAt?.toString()}`);
  const datestring =
    date.getDate() +
    "-" +
    (date.getMonth() + 1) +
    "-" +
    date.getFullYear() +
    " " +
    date.getHours() +
    ":" +
    date.getMinutes();

  const activityDelete = () => {
    if (window.confirm("¿Estas seguro que deseas borrar esta actividad?")) {
      axiosInstance.delete("/Expense/" + id).then((response) => {
        alert("¡Actividad eliminada exitosamente!");
        window.location.reload();
      });
    }
  };

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
        <Avatar sx={{ bgcolor: "#1769aa" }} variant="rounded">
          {Services[serviceId].name.slice(0, 1)}
        </Avatar>

        <Grid sx={{ marginLeft: 1, display: "flex", flexDirection: "column" }}>
          <Typography fontWeight={500}>{Services[serviceId].name}</Typography>
          <Typography
            color="textSecondary"
            sx={{ cursor: "pointer", fontSize: 12 }}
            onClick={() => {
              window.open(Services[serviceId].homePage);
            }}
          >
            {Services[serviceId].homePage}
          </Typography>
          <Typography color="textSecondary" sx={{ fontSize: 12 }}>
            {datestring}
          </Typography>
        </Grid>
      </Grid>

      <Grid container item xs={6} alignItems="center" justifyContent="flex-end">
        <IconButton
          aria-label="delete"
          onClick={activityDelete}
          sx={{ marginRight: 2 }}
          color="error"
          title="Borrar"
        >
          <DeleteIcon />
        </IconButton>

        <IconButton
          aria-label="delete"
          sx={{ marginRight: 2 }}
          color="info"
          title="Editar"
          onClick={() => {
            navigate(`/activity?edit=${id}&org=${organizationId}`);
          }}
        >
          <EditIcon />
        </IconButton>

        <Divider orientation="vertical" flexItem sx={{ marginRight: 2 }} />

        <Typography fontWeight={500} variant="h6">
          {currencies[currencyId].symbol + amount}
        </Typography>
      </Grid>
    </Grid>
  );
};

export default ActivityCard;
