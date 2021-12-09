import { useContext } from "react";
import { AppContext } from "../contexts/AppContext";

export const useAppContext = (): any => {
  return useContext(AppContext);
};
