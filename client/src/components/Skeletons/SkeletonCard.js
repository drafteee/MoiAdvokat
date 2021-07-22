import React from "react";
import { Card, Skeleton } from "antd";
import "./style.css"

export default function SkeletonCard() {
    return (
        <Card
            className={"sceletonCard"}
            style={{
                margin: "1%",
            }}
            size={"default"}
        >
            <Skeleton loading={true} active />

        </Card>)
}