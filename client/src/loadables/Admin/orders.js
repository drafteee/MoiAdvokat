import Loadable from '@axiomhq/react-loadable'
import React from 'react'
import Loading from '../../components/Loading/Global'

const LoadableAdminListAllOrders = Loadable({
	loader  : () => import('../../containers/Admin/Orders/ListAll'),
	loading : () => <Loading />
})

export const AdminOrdersLoadables = {
    LoadableAdminListAllOrders
}
