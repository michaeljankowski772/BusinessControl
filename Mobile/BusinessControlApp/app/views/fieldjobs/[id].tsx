import { useLocalSearchParams } from "expo-router";
import { useEffect, useState } from "react";
import { Button, Text, TextInput, View } from "react-native";
import { getFieldJobById, updateFieldJob } from "../../api/fieldjob";

export default function JobDetails() {

  const { id } = useLocalSearchParams();
  const [job, setJob] = useState<any>(null);

  useEffect(() => {
    loadJob();
  }, []);

  const loadJob = async () => {
    const data = await getFieldJobById(Number(id));
    setJob(data);
  };

  const handleSave = async () => {
    await updateFieldJob(job);
    alert("Saved");
  };

  if (!job) return <Text>Loading...</Text>;

  return (
    <View style={{ padding: 20 }}>
      <Text>ID: {job.id}</Text>

      <TextInput
        value={job.name}
        onChangeText={(text) => setJob({ ...job, name: text })}
        style={{ borderWidth: 1, marginBottom: 10 }}
      />

      <Button title="Save" onPress={handleSave} />
    </View>
  );
}