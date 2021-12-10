// Add comma to numbers
export const thousands_separators = (x: number | string) => {
  return x.toString().replace(/^[+-]?\d+/, (int: string) => {
    return int.replace(/(\d)(?=(\d{3})+$)/g, "$1,");
  });
};
