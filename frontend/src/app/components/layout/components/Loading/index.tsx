import Grid from "@mui/material/Grid";
import CircularProgress from "@mui/material/CircularProgress";

const Loading = () => {
  return (
    <Grid
      container
      alignItems="center"
      justifyContent="center"
      sx={{ height: "100vh" }}
    >
      <CircularProgress size={60} />
    </Grid>
  );
};

export default Loading;
