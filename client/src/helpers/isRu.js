export default function isRu() {
  return window.localStorage.getItem("language") === "ru" ? 1 : 0;
}
