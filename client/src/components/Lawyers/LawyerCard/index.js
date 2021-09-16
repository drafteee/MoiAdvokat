import React from 'react'
import { Avatar, Card, Skeleton } from 'antd'

import Global from '../../../Global'

import i18nGlobal from '../../../localization'

import 'antd/lib/card/style/css'
import 'antd/lib/skeleton/style/css'
import 'antd/lib/avatar/style/css'

const serverUrl =
  process.env.NODE_ENV === 'development'
    ? Global.SERVER_API_URL
    : window.location.origin + '/api'

const LawyerCard = ({ className, lawyer, lawyerLoading, isRu, children }) => {
    return (
        <Card
            className={className}
            actions={children}>
            <Skeleton
                avatar
                active={true}
                loading={lawyerLoading}>
                {lawyer ? (
                    <>
                        <Card.Meta
                            avatar={
                                <Avatar size="large">{`${lawyer.LastName[0]}${lawyer.FirstName[0]}${lawyer.MiddleName[0] ?? ""}}`}</Avatar>
                            }
                            title={`${lawyer.LastName} ${lawyer.FirstName} ${lawyer.MiddleName ?? ""}`} />

                        <a href={`${serverUrl}/File/DownloadFile?Id=${lawyer.FileId}`}>
                            {`${i18nGlobal.download[isRu]}: ${lawyer.FileName}`}
                        </a>
                    </>
                ) : null}
            </Skeleton>
        </Card>
    )
}

export default LawyerCard