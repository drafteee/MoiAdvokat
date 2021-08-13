import Loadable from '@axiomhq/react-loadable'
import React from 'react'
import Loading from '../components/Loading/Global'

const LoadableLogin = Loadable({
	loader  : () => import('../containers/Login'),
	loading : () => <Loading />
})

const LoadableAccount = Loadable({
	loader  : () => import('../containers/Account'),
	loading : () => <Loading />
})

export const accountLoadables = {
	LoadableLogin,
	LoadableAccount
}
