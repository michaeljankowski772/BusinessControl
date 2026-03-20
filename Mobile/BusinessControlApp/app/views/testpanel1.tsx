import AsyncStorage from "@react-native-async-storage/async-storage";
import { useEffect, useState } from "react";
import { Button, FlatList, StyleSheet, Text, View } from "react-native";

export default function Dashboard() {

  const [jobs, setJobs] = useState<any[]>([]);
  const [menuVisible, setMenuVisible] = useState(false);

  useEffect(() => {
    loadJobs();
  }, []);

  async function loadJobs() {

    const token = await AsyncStorage.getItem("token");

    const response = await fetch("http://192.168.0.171:5006/fieldjobs/getfieldjobs", {
      headers: {
        Authorization: "Bearer " + token
      }
    });

    const data = await response.json();

    setJobs(data);
  }

  return (
    <View style={styles.container}>

      {/* przycisk menu */}
      <Button title="Menu" onPress={() => setMenuVisible(!menuVisible)} />

      {/* wysuwany panel */}
      {menuVisible && (
        <View style={styles.sidebar}>
          <Text style={styles.menuItem}>Jobs: {jobs.length}</Text>
          <Text style={styles.menuItem}>Users: 5</Text>
          <Text style={styles.menuItem}>Settings</Text>
        </View>
      )}

      <Text style={styles.title}>Workshop Jobs</Text>

      {/* tabela */}
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