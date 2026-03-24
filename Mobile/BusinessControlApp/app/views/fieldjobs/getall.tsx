import { useRouter } from "expo-router";
import { useEffect, useState } from "react";
import {
  ActivityIndicator,
  RefreshControl,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";
import { SwipeListView } from "react-native-swipe-list-view";
import {
  getFieldJobsWithHeaders
} from "../../api/fieldjob";
import {
  COLUMNS,
  FieldJob,
  FieldJobsResponse
} from "../../helpers/translations";

export default function Dashboard() {
  const [jobs, setJobs] = useState<FieldJob[]>([]);
  const [columns, setColumns] = useState<string[]>([]);
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

      const res: FieldJobsResponse = await getFieldJobsWithHeaders();
      setColumns(res.columns); 
      setJobs(res.data);       
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
      {}
      <View style={styles.headerRow}>
        {columns.map((c) => (
          <Text key={c} style={styles.headerCell}>{c}</Text>
        ))}
      </View>

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
            {COLUMNS.map((col) => (
              <Text key={col.key} style={styles.cell}>
                {item[col.key]}
              </Text>
            ))}
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
  center: { flex: 1, justifyContent: "center", alignItems: "center" },
  container: { flex: 1, padding: 20 },
  headerRow: {
    flexDirection: "row",
    backgroundColor: "#eee",
    borderBottomWidth: 2,
    borderBottomColor: "#ccc",
    paddingVertical: 10,
  },
  headerCell: {
    flex: 1,
    fontWeight: "bold",
    paddingHorizontal: 10,
    textAlign: "center",
    borderRightWidth: 1,
    borderRightColor: "#ccc",
  },
  row: {
    flexDirection: "row",
    paddingVertical: 15,
    backgroundColor: "#fff",
    borderBottomWidth: 1,
    borderBottomColor: "#eee",
  },
  cell: {
    flex: 1,
    paddingHorizontal: 10,
    textAlign: "center",
    borderRightWidth: 1,
    borderRightColor: "#eee",
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
});