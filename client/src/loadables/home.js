import Loadable from '@axiomhq/react-loadable'
import React from 'react'
import Loading from '../components/Loading/Global'

const LoadableHome = Loadable({
	loader  : () => import('../containers/Home'),
	loading : () => <Loading />
})

// const LoadableContact = Loadable({
// 	loader  : () => import('../containers/Contacts'),
// 	loading : () => <Loading />
// })

export const homeLoadables = {
	LoadableHome,
	// LoadableContact
}
