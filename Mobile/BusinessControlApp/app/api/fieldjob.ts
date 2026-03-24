import { FieldJob, FieldJobsResponse } from "../helpers/translations";
import { apiFetch } from "./client";



export const columnKeyMap: Record<string, keyof FieldJob> = {
    Id: "id",
    Name: "name",
    Status: "status",
  };


export const getFieldJobs = async (): Promise<FieldJob[]> => {
  const response = await apiFetch("/fieldjobs/getfieldjobs");

  if (!response.ok) {
    throw new Error(`HTTP error: ${response.status}`);
  }

  return response.json();
};

export const getFieldJobsWithHeaders = async (): Promise<FieldJobsResponse> => {


  const response = await apiFetch("/fieldjobs/getfieldjobswithheaders");
  if (!response.ok) {
    throw new Error(`HTTP error: ${response.status}`);
  }

  const result: FieldJobsResponse = await response.json();
        console.log("FieldJobsResponse", result);

  return result;
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