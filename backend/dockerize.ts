import { spawn } from "child_process";

const PROD = process.env.NODE_ENV === "prod";
const PORT = process.env.PORT || 5000;

async function main() {
    const dir = PROD ? "Release" : "Debug";
    const env = PROD ? "Production" : "Development";
    const dockerfile = PROD ? "Dockerfile.server.prod" : "Dockerfile.server.dev";

    console.log("1. Publishing dotnet...")
    await $(`dotnet publish -c ${dir} -o ./bin/Publish/${dir}/net6.0/`)

    console.log("2. Creating docker image...")
    await $(`docker build -t iso810-erp -f ${dockerfile} .`)

    console.log("3. Running docker image...")
    await $(`docker run -e ASPNETCORE_ENVIRONMENT=${env} -e ASPNETCORE_URLS=http://+:${PORT} --env-file=../.env -dp ${PORT}:${PORT} --rm iso810-erp`)
}

// Run
main();

// Helpers

async function $(cmd: string, args: string[] = []) {
  return new Promise((resolve, reject) => {
    const child = spawn(cmd, args, {
      stdio: "inherit",
      shell: true,
    });
    child.on("close", (code) => {
      if (code !== 0) {
        reject(new Error(`${cmd} ${args.join(" ")} failed with code ${code}`));
      } else {
        resolve(null);
      }
    });
  });
}
