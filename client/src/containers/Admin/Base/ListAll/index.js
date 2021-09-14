import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Table, Tooltip, Popconfirm, Button } from 'antd'
import {
    DeleteOutlined,
    UndoOutlined
} from '@ant-design/icons'

import { baseActions } from '../store/actions'

import 'antd/lib/table/style/css'

import i18nGlobal from '../../../../localization'

const ListAll = ({ controller, updateTrigger, columns }) => {
    const { currentList } = useSelector(state => state.baseReducer)
    const { isRu } = useSelector(state => state.globalReducer)

    const dispatch = useDispatch()

    useEffect(() => {
        if (controller) {
            dispatch(baseActions.getAll(controller))
        }
    }, [controller, updateTrigger])

    const softDelete = id => {
        if (controller) {
            dispatch(baseActions.softDelete({
                controller, id
            }))
        }
    }

    const restore = id => {
        if (controller) {
            dispatch(baseActions.restore({
                controller, id
            }))
        }
    }

    // const baseColumns = {}

    // Object.assign(baseColumns, columns)

    // baseColumns[Object.keys(baseColumns).length] = {
    //     title: i18nGlobal.actions[isRu],
    //     key: 'action',
    //     render: (_, record) => (
    //         <>
    //             {record.IsDeleted ? (
    //                 <Tooltip title={i18nGlobal.restore[isRu]}>
    //                     <Popconfirm
    //                         title={i18nGlobal.areYouSureMessage[isRu]}
    //                         onConfirm={() => restore(record.id)}
    //                         okText={i18nGlobal.yes[isRu]}
    //                         cancelText={i18nGlobal.no[isRu]}
    //                     >
    //                         <Button>
    //                             <UndoOutlined />
    //                         </Button>
    //                     </Popconfirm>
    //                 </Tooltip>
    //             ) : (
    //                 <Tooltip title={i18nGlobal.delete[isRu]}>
    //                     <Popconfirm
    //                         title={i18nGlobal.areYouSureMessage[isRu]}
    //                         onConfirm={() => softDelete(record.id)}
    //                         okText={i18nGlobal.yes[isRu]}
    //                         cancelText={i18nGlobal.no[isRu]}
    //                     >
    //                         <Button>
    //                             <DeleteOutlined />
    //                         </Button>
    //                     </Popconfirm>
    //                 </Tooltip>
    //             )}
    //         </>
    //     )
    // }

    return (
        <>
            <Table columns={columns} dataSource={currentList} />
        </>
    )
}

export default ListAll