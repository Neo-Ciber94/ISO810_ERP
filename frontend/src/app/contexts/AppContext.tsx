import { FC, createContext, useMemo, useState } from "react";

interface UserData {
  id?: number;
  name?: string;
  email?: string;
  isAuthenticated: boolean;
}

export interface AppContextType {
  user: UserData;
  updateUserData: any;
}

const config: AppContextType = {
  user: {
    id: 0,
    name: "",
    email: "",
    isAuthenticated: false,
  },
  updateUserData: () => {},
};

export const AppContext = createContext<AppContextType>(config);

export const AppContextProvider: FC = ({ children }) => {
  const [user, updateUserData] = useState<AppContextType>(config);
  const value: any = useMemo(() => ({ user, updateUserData }), [user]);

  return <AppContext.Provider value={value}>{children}</AppContext.Provider>;
};
