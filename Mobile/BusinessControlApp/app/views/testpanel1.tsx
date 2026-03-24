import { useEffect, useState } from "react";
import { Button, FlatList, StyleSheet, Text, View } from "react-native";
import { FieldJob, getFieldJobs } from "../api/fieldjob";

export default function Dashboard() {

  const [jobs, setJobs] = useState<FieldJob[]>([]);
  const [menuVisible, setMenuVisible] = useState(false);
  const [error, setError] = useState<string>("");

  useEffect(() => {
    loadJobs();
  }, []);

  const loadJobs = async () => {
    try {
      const data = await getFieldJobs();
      setJobs(data);
    } catch (err: any) {
      console.log("API ERROR:", err);
      setError(err.message);
    }
  };

  return (
    <View style={styles.container}>

      <Button title="Menu" onPress={() => setMenuVisible(!menuVisible)} />

      {menuVisible && (
        <View style={styles.sidebar}>
          <Text style={styles.menuItem}>Jobs: {jobs.length}</Text>
          <Text style={styles.menuItem}>Users: 5</Text>
          <Text style={styles.menuItem}>Settings</Text>
        </View>
      )}

      <Text style={styles.title}>Workshop Jobs</Text>

      {error ? <Text style={{ color: "red" }}>{error}</Text> : null}

      <FlatList
        data={jobs}
        keyExtractor={(item) => item.id.toString()}
        renderItem={({ item }) => (
          <View style={styles.row}>
            <Text style={styles.cell}>{item.id}</Text>
            <Text style={styles.cell}>{item.name}</Text>
          </View>
        )}
      />

    </View>
  );
}
const styles = StyleSheet.create({

  container: {
    flex: 1,
    padding: 20
  },

  title: {
    fontSize: 22,
    marginBottom: 10
  },

  row: {
    flexDirection: "row",
    borderBottomWidth: 1,
    padding: 10
  },

  cell: {
    flex: 1
  },

  sidebar: {
    position: "absolute",
    left: 0,
    top: 0,
    bottom: 0,
    width: 200,
    backgroundColor: "#eee",
    padding: 20
  },

  menuItem: {
    marginBottom: 10,
    fontSize: 18
  }

});