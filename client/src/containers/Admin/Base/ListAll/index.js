import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Table, Tooltip, Popconfirm, Button } from 'antd'
import {
    DeleteOutlined,
    UndoOutlined
} from '@ant-design/icons'

import { baseActions } from '../store/actions'

import 'antd/lib/table/style/css'
import 'antd/lib/tooltip/style/css'
import 'antd/lib/popconfirm/style/css'

import i18nGlobal from '../../../../localization'

const ListAll = ({ controller, columns }) => {
    const { currentList, updateTrigger } = useSelector(state => state.baseReducer)
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

    const baseColumns = [
        ...columns,
        {
            title: i18nGlobal.actions[isRu],
            key: 'action',
            render: (_, record) => (
                <>
                    {record.IsDeleted ? (
                        <Tooltip title={i18nGlobal.restore[isRu]}>
                            <Popconfirm
                                title={i18nGlobal.areYouSureMessage[isRu]}
                                onConfirm={() => restore(record.Id)}
                                okText={i18nGlobal.yes[isRu]}
                                cancelText={i18nGlobal.no[isRu]}
                            >
                                <Button>
                                    <UndoOutlined />
                                </Button>
                            </Popconfirm>
                        </Tooltip>
                    ) : (
                        <Tooltip title={i18nGlobal.delete[isRu]}>
                            <Popconfirm
                                title={i18nGlobal.areYouSureMessage[isRu]}
                                onConfirm={() => softDelete(record.Id)}
                                okText={i18nGlobal.yes[isRu]}
                                cancelText={i18nGlobal.no[isRu]}
                            >
                                <Button>
                                    <DeleteOutlined />
                                </Button>
                            </Popconfirm>
                        </Tooltip>
                    )}
                </>
            )
        }
    ]

    return (
        <>
            <Table columns={baseColumns} dataSource={currentList} />
        </>
    )
}

export default ListAll