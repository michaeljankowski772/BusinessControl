import { useRouter } from "expo-router";
import { useEffect, useState } from "react";
import {
  ActivityIndicator,
  Button,
  RefreshControl,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";
import { SwipeListView } from "react-native-swipe-list-view";
import {
  FieldJob,
  getFieldJobs,
} from "../../api/fieldjob";

export default function Dashboard() {
  const [jobs, setJobs] = useState<FieldJob[]>([]);
  const [loading, setLoading] = useState(true);
  const [refreshing, setRefreshing] = useState(false);
  const [error, setError] = useState("");

  const router = useRouter();

  useEffect(() => {
    loadJobs(true);
  }, []);

  const loadJobs = async (initial = false) => {
    try {
      if (initial) setLoading(true);

      const data = await getFieldJobs();
      setJobs(data);
    } catch (err: any) {
      console.log("API ERROR:", err);
      setError(err.message);
    } finally {
      setLoading(false);
      setRefreshing(false);
    }
  };

  const onRefresh = async () => {
    setRefreshing(true);
    await loadJobs();
  };

  /* const handleDelete = async (id: number) => {
    try {
      await deleteFieldJob(id);

      setJobs((prev) => prev.filter((j) => j.id !== id));
    } catch (err) {
      console.log("DELETE ERROR", err);
    }
  }; */

  if (loading) {
    return (
      <View style={styles.center}>
        <ActivityIndicator size="large" />
        <Text>Loading jobs...</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <View style={styles.topBar}>
        <Button title="Add Job" onPress={() => router.push("./create")} />
      </View>

      {error ? <Text style={{ color: "red" }}>{error}</Text> : null}

      <SwipeListView
        data={jobs}
        keyExtractor={(item) => item.id.toString()}

        refreshControl={
          <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
        }

        renderItem={({ item }) => (
          <TouchableOpacity
            style={styles.row}
            onPress={() => router.push(`./${item.id}`)}
          >
            <Text style={styles.cell}>{item.id}</Text>
            <Text style={styles.cell}>{item.name}</Text>
          </TouchableOpacity>
        )}

        renderHiddenItem={({ item }) => (
          <View style={styles.hiddenRow}>
            <TouchableOpacity
              style={styles.deleteButton}
            //onPress={() => handleDelete(item.id)}
            >
              <Text style={{ color: "white" }}>Delete</Text>
            </TouchableOpacity>
          </View>
        )}

        rightOpenValue={-80} 
      />
    </View>
  );
}


const styles = StyleSheet.create({
  center: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
  },

  topBar: {
    flexDirection: "row",
    justifyContent: "flex-end",
    marginBottom: 10,
  },

  row: {
    flexDirection: "row",
    padding: 15,
    borderBottomWidth: 1,
    backgroundColor: "#fff",
  },

  cell: {
    flex: 1,
  },

  hiddenRow: {
    alignItems: "flex-end",
    justifyContent: "center",
    flex: 1,
    backgroundColor: "red",
    paddingRight: 15,
  },

  deleteButton: {
    backgroundColor: "red",
    padding: 10,
    borderRadius: 5,
  },

  container: {
    flex: 1,
    padding: 20
  },

  title: {
    fontSize: 22,
    marginBottom: 10
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