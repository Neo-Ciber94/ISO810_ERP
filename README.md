# ISO810 ERP

## Setup

1. Install [Node.js](https://nodejs.org/en/)

2. Install [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)

3. Install node packages

    ```codecopy
    npm install
    ```

4. Install `frontend` node packages

    ```codecopy
    npm install --prefix ./frontend ./frontend
    ```

5. Restore `backend` dotnet nuget packages

    ```codecopy
    dotnet restore ./backend
    ```

## Run

1. Run both the server and client in development mode

    ```codecopy
    npm run dev
    ```

    You can also run both individually using `npm run dev:server` and `npm run dev:client`.
