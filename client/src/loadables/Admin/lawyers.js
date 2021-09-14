import Loadable from '@axiomhq/react-loadable'
import React from 'react'
import Loading from '../../components/Loading/Global'

const LoadableAdminListAllLawyers = Loadable({
	loader  : () => import('../../containers/Admin/Lawyers/ListAll'),
	loading : () => <Loading />
})

export const AdminLawyersLoadables = {
    LoadableAdminListAllLawyers
}
