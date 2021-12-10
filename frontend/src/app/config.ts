import axios from "axios";

export const axiosInstance = axios.create({
  baseURL: "http://localhost:5000/api",
  withCredentials: true,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json",
  },
});

export const UserStorageKey = "UserStorageKey";
