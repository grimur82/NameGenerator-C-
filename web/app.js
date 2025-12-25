const result = document.getElementById("result");
const historyList = document.getElementById("history");
const generateButton = document.getElementById("generate");
const clearButton = document.getElementById("clear");

const addHistoryItem = (name) => {
  const item = document.createElement("li");
  item.textContent = name;
  historyList.prepend(item);
};

const setLoading = (isLoading) => {
  generateButton.disabled = isLoading;
  generateButton.textContent = isLoading ? "Generating..." : "Generate Name";
};

const fetchName = async () => {
  setLoading(true);
  result.textContent = "Working...";

  try {
    const response = await fetch("/api/name");
    if (!response.ok) {
      throw new Error("Server error");
    }
    const data = await response.json();
    result.textContent = data.name;
    addHistoryItem(data.name);
  } catch (error) {
    result.textContent = "Unable to generate a name. Try again.";
  } finally {
    setLoading(false);
  }
};

generateButton.addEventListener("click", fetchName);
clearButton.addEventListener("click", () => {
  result.textContent = "Click generate to get a name.";
  historyList.innerHTML = "";
});
