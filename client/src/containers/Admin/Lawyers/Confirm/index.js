import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { Tooltip, Button, Alert } from 'antd'
import { CheckCircleFilled, CloseCircleFilled } from '@ant-design/icons'

import LawyerCard from '../../../../components/Lawyers/LawyerCard'

import { lawyerActions } from '../../../Lawyers/store/actions'

import i18nGlobal from '../../../../localization'

import 'antd/lib/tooltip/style/css'
import 'antd/lib/button/style/css'
import 'antd/lib/alert/style/css'

import './style.css'

const ConfirmLawyer = props => {
    const id = props.match.params.id

    const { lawyer, isLoading, canLawyerBeConfirmed } = useSelector(state => state.lawyerReducer)
    const { isRu } = useSelector(state => state.globalReducer)

    const dispatch = useDispatch()

    useEffect(() => {
        if (id) {
            dispatch(lawyerActions.checkIfLawyerCanBeConfirmed({ id }))
        }
    }, [id])

    useEffect(() => {
        if (canLawyerBeConfirmed) {
            dispatch(lawyerActions.getById({ id }))
        }
    }, [canLawyerBeConfirmed, id])

    const confirmLawyer = isVerified => {
        dispatch(lawyerActions.confirmLawyer({
            id,
            isVerified
        }))
    }

    return (
        canLawyerBeConfirmed ? (
            <LawyerCard
                className="custom-card"
                lawyer={lawyer}
                lawyerLoading={isLoading}
                isRu={isRu}>
                <Tooltip title={i18nGlobal.accept[isRu]}>
                    <Button onClick={() => confirmLawyer(true)}>
                        <CheckCircleFilled style={{ color: '#27ae60' }} />
                    </Button>
                </Tooltip>
                <Tooltip title={i18nGlobal.decline[isRu]}>
                    <Button onClick={() => confirmLawyer(false)}>
                        <CloseCircleFilled style={{ color: '#e74c3c' }} />
                    </Button>
                </Tooltip>
            </LawyerCard>
        ) : (
            <Alert type="error" message={i18nGlobal.actionUnavailable[isRu]} showIcon />
        )
    )
}

export default ConfirmLawyer