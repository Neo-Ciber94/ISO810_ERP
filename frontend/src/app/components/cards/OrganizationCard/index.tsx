import { useNavigate } from "react-router-dom";
import { axiosInstance } from "../../../config";

import Grid from "@mui/material/Grid";
import Avatar from "@mui/material/Avatar";
import Typography from "@mui/material/Typography";
import IconButton from "@mui/material/IconButton";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";

import * as Models from "../../../../models";

const OrganizationCard = ({
  id,
  accountId,
  name,
  alias,
  createdAt,
}: Models.Organization) => {
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

  const organizationDelete = () => {
    if (
      window.confirm(
        "¿Estas seguro que deseas borrar esta organización? \nAl borrar esta organización también borrará todas las actividades relacionadas a esta entidad."
      )
    ) {
      axiosInstance.delete("/Organization/" + id).then((response) => {
        alert("Organización eliminada exitosamente!");
        navigate("/dashboard");
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
        <Avatar sx={{ bgcolor: "#482880" }} variant="rounded">
          {name.slice(0, 1)}
        </Avatar>

        <Grid sx={{ marginLeft: 1, display: "flex", flexDirection: "column" }}>
          <Typography fontWeight={500}>{name}</Typography>
          <Typography color="textSecondary" sx={{ fontSize: 12 }}>
            Alias: {alias}
          </Typography>
          <Typography color="textSecondary" sx={{ fontSize: 12 }}>
            {datestring}
          </Typography>
        </Grid>
      </Grid>

      <Grid container item xs={6} alignItems="center" justifyContent="flex-end">
        <IconButton
          onClick={organizationDelete}
          sx={{ marginRight: 2 }}
          color="error"
          title="Borrar"
        >
          <DeleteIcon />
        </IconButton>

        <IconButton
          sx={{ marginRight: 2 }}
          color="info"
          title="Editar"
          onClick={() => {
            navigate(`/organization/form?edit=${id}&acc=${accountId}`);
          }}
        >
          <EditIcon />
        </IconButton>
      </Grid>
    </Grid>
  );
};

export default OrganizationCard;
