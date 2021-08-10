import Loadable from '@axiomhq/react-loadable'
import React from 'react'
import Loading from '../components/Loading/Global'

const LoadableLawyersList = Loadable({
	loader  : () => import('../containers/Lawyers'),
	loading : () => <Loading />
})

const LoadableOrdersList = Loadable({
	loader  : () => import('../containers/Orders'),
	loading : () => <Loading />
})

const LoadableOrder = Loadable({
	loader  : () => import('../containers/Orders/Order'),
	loading : () => <Loading />
})


export const BLLoadables = {
	LoadableLawyersList,
  LoadableOrdersList,
  LoadableOrder
}
