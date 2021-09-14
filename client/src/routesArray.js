
import { homeLoadables, accountLoadables, BLLoadables, OrdersLoadables } from "./loadables"
import NotFound from "./components/NotFound"
import { UnauthorizedAccess } from "./components/UnauthorizedAccess"


export default [
  {
    path: "/",
    component: homeLoadables.LoadableHome
  },
  {
    path: "/login",
    component: accountLoadables.LoadableLogin
  },
  {
    path: "/account",
    component: accountLoadables.LoadableAccount,
    protect: (user) => user,
    redirectTo: UnauthorizedAccess
  },
  {
    path: "/lawyers",
    component: BLLoadables.LoadableLawyersList
  },
  {
    path: "/orders",
    component: BLLoadables.LoadableOrdersList,
    protect: (user) => user
  },
  {
    path: "/responses/:orderId(\d+)",
    component: OrdersLoadables.LoadableListResponses,
    protect: (user) => user
  },
  {
    path: "/order/:id",
    component: BLLoadables.LoadableOrder,
    protect: (user) => user
  },
  {
    path: "/report",
    component: BLLoadables.LoadableReport,
    protect: (user) => user
  },
  {
    path: "/submitOrder",
    component: OrdersLoadables.LoadableSubmitOrder,
    protect: (user) => user
  },
  {
    path: "*",
    component: NotFound
  },
  {
    path: "/order/execute/:id",
    component: BLLoadables.LoadableExecuteOrder
  }
]