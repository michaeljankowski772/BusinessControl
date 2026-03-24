export type FieldJob = {
  id: number;
  customerFirstName: string;
  customerLastName: string;
  workerFirstName: string;
  workerLastName: string;
  fieldArea: number;
};

export type FieldJobsResponse = {
  columns: string[];
  data: FieldJob[];
};

 export const COLUMNS: { key: keyof FieldJob; label: string }[] = [
    { key: "id", label: "ID" },
    { key: "customerFirstName", label: "Imie klienta" },
    { key: "customerLastName", label: "Nazwisko klienta" },
    { key: "workerFirstName", label: "Imie pracownika" },
    { key: "workerLastName", label: "Nazwisko pracownika" },
    { key: "fieldArea", label: "Pole" },
  ];