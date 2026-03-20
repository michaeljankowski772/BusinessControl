/* import { Text, View } from "react-native";

export default function Index() {
  return (
    <View
      style={{
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <Text>Edit app/index.tsx to edit this screen.</Text>
    </View>
  );
}
 */

import { Link } from "expo-router";
import { useEffect, useState } from "react";
import { Text, View } from "react-native";

export default function App() {

  const [jobs, setJobs] = useState([]);
  const [error, setError] = useState(null);
  const API_URL = process.env.EXPO_PUBLIC_API_URL;

  useEffect(() => {

    async function loadData() {

      try {

        console.log(API_URL);
        const response = await fetch(`${API_URL}/workshopjob/getworkshopjobs`);

        if (!response.ok) {
          throw new Error("HTTP error: " + response.status);
        }

        const data = await response.json();

        setJobs(data);

      } catch (err) {

        console.log("API ERROR:", err);
        setError(err.message);

      }

    }

    loadData();

  }, []);

  if (error) {
    return (
      <View>
        <Text>ERROR: {error}</Text>
      </View>
    );
  }

  return (
    <View>
      {jobs.map(job => (
        <Text key={job.id}>{job.id}</Text>
      ))}
      <Text>{jobs.length} jobs count</Text>
      <Link href={"/views/login"}>Go to Login</Link>
    </View>
  );
}