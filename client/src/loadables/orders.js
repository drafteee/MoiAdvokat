import Loadable from '@axiomhq/react-loadable'
import React from 'react'
import Loading from '../components/Loading/Global'

const LoadableSubmitOrder = Loadable({
	loader  : () => import('../containers/Orders/Submit'),
	loading : () => <Loading />
})

const LoadableListResponses = Loadable({
	loader  : () => import('../containers/Orders/ListOrderResponse'),
	loading : () => <Loading />
})

export const OrdersLoadables = {
    LoadableSubmitOrder,
		LoadableListResponses
}
