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

import { useEffect, useState } from "react";
import { Text, View } from "react-native";

export default function App() {

  const [jobs, setJobs] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {

    async function loadData() {

      try {

        const response = await fetch("https://192.168.0.171:7242/workshopjobs/getworkshopjobs");

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
      {<Text>{jobs.length} jobs</Text>}
    </View>
  );
}