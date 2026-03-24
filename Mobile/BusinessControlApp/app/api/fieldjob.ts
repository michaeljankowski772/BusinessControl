import { apiFetch } from "./client";

export type FieldJob = {
  id: number;
  name: string;
};

export const getFieldJobs = async (): Promise<FieldJob[]> => {
  const response = await apiFetch("/fieldjobs/getfieldjobs");

  if (!response.ok) {
    throw new Error(`HTTP error: ${response.status}`);
  }

  return response.json();
};

//todo implement cache
/*let jobsCache: FieldJob[] | null = null;

 export const getFieldJobs = async (forceRefresh = false) => {
  if (jobsCache && !forceRefresh) {
    return jobsCache;
  }

  const data = await apiFetch("/fieldjobs/getfieldjobs");

  jobsCache = data;
  return data;
}; */

export const getFieldJobById = (id: number) =>
  apiFetch(`/fieldjobs/${id}`);

export const updateFieldJob = (job: FieldJob) =>
  apiFetch(`/fieldjobs/${job.id}`, {
    method: "PUT",
    body: JSON.stringify(job),
  });

export const createFieldJob = (job: Partial<FieldJob>) =>
  apiFetch(`/fieldjobs`, {
    method: "POST",
    body: JSON.stringify(job),
  });