import Box from "@mui/material/Box";
import Drawer from "@mui/material/Drawer";
import SidebarContent from "../SidebarContent";

interface SidebarProps {
  isOpen: boolean;
  onOclose: any;
}

const Sidebar = ({ isOpen, onOclose }: SidebarProps) => {
  return (
    <Drawer anchor="right" open={isOpen} onClose={onOclose}>
      <Box
        sx={{ width: 250, paddingTop: 5, paddingX: 2 }}
        role="presentation"
        flexDirection="column"
        justifyContent="flex-start"
        alignItems="flex-start"
      >
        <SidebarContent externalFunction={onOclose} />
      </Box>
    </Drawer>
  );
};

export default Sidebar;
