import { createBrowserHistory } from "history";

const history = createBrowserHistory();
// const prevHistoryPush = history.push
// let lastLocation = history.location

// history.listen(location => {
//   lastLocation = location
// })

let prevLocation = {};
history.listen((location) => {
  const pathChanged = prevLocation.pathname !== location.pathname;
  const hashChanged = prevLocation.hash !== location.hash;
  if (pathChanged || hashChanged) window.scrollTo(0, 0);
  prevLocation = location;
});

// history.push = (pathname, state = {}) => {
//   //     lastLocation.pathname + lastLocation.search + lastLocation.hash, JSON.stringify(state) !== JSON.stringify(lastLocation.state))
//   if (
//     lastLocation === null ||
//         pathname !==
//         lastLocation.pathname + lastLocation.search + lastLocation.hash ||
//         JSON.stringify(state) !== JSON.stringify(lastLocation.state)
//   ) {
//     if (pathname !== '#')
//       prevHistoryPush(pathname, state)
//   }
// }
export default history;
