import { FieldJob, FieldJobsResponse } from "../helpers/translations";
import { apiFetch } from "./client";

export const getFieldJobs = async (): Promise<FieldJob[]> => {
  const response = await apiFetch("/fieldjobs/getfieldjobs");


  return response.json();
};

export const getFieldJobsWithHeaders = async (): Promise<FieldJobsResponse> => {


  const response = await apiFetch("/fieldjobs/getfieldjobswithheaders");
  

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

export const getFieldJobById = async (id: number): Promise<FieldJob> => {
  const response = await apiFetch(`/fieldjobs/${id}`);
  const result: FieldJob = await response.json();
        console.log("FieldJob", result);

  return result;
};


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

  export const createEmptyFieldJob = async () => {
  const response = await apiFetch(`/fieldjobs`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({})
  });

  return await response.json();
};