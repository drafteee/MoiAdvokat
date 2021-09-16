
import { homeLoadables, accountLoadables, BLLoadables, OrdersLoadables, AdminLawyersLoadables } from "./loadables"
import NotFound from "./components/NotFound"
import { UnauthorizedAccess } from "./components/UnauthorizedAccess"
import { AdminOrdersLoadables } from "./loadables/Admin/orders"


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
    path: "/order/execute/:id",
    component: BLLoadables.LoadableExecuteOrder
  },
  {
    path: "/admin/lawyers/listAll",
    component: AdminLawyersLoadables.LoadableAdminListAllLawyers
  },
  {
    path: "/admin/orders/listAll",
    component: AdminOrdersLoadables.LoadableAdminListAllOrders
  },
  { // TODO: сверять id пользователя, чтобы понимать, что адвокат сам загружает файл, а не кто-то другой
    path: "/uploadLawyerCertificate/:id",
    component: BLLoadables.LoadableUploadLawyerCertificate
  },
  {
    path: "*",
    component: NotFound
  }
]