import Loadable from '@axiomhq/react-loadable'
import React from 'react'
import Loading from '../components/Loading/Global'

const LoadableChatTest = Loadable({
	loader  : () => import('../containers/ChatTestPage'),
	loading : () => <Loading />
})

export const chatLoadables = {
	LoadableChatTest,
}
