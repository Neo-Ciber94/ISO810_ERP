# Getting Started with Create React App

http://localhost:5000/api/currency

        <FormControl fullWidth>
            <InputLabel>Servicio</InputLabel>
            <Select value={activityData.organizationId} label="Servicio">
              <MenuItem value={0}>
                Selecciona la empresa que ofrece el servicio
              </MenuItem>
              {/* {activityData.Organizations.map((Organization) => {
                const { id, alias } = Organization;
                return (
                  <MenuItem key={id} value={id}>
                    {alias}
                  </MenuItem>
                );
              })} */}
            </Select>
          </FormControl>
