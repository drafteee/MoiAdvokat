import React, { useEffect, useState } from "react";
import { Switch, Route } from "react-router-dom";
import {
  homeLoadables,
  accountLoadables,
  BLLoadables,
  chatLoadables,
} from "./loadables";
import NotFound from "./components/NotFound";
import { useSelector, useDispatch } from "react-redux";
import { userActions } from "./store/actions";

const Routes = () => {
  const dispatch = useDispatch();
  const { user, isLoading, roles, userTypeName } = useSelector(
    (state) => state.userReducer
  );
    
  useEffect(() => {
    if (user === undefined) dispatch(userActions.refreshUserData());
  }, []);

  return (
    <Switch>
      <Route exact component={homeLoadables.LoadableHome} path="/" />
      <Route exact component={accountLoadables.LoadableLogin} path="/login" />
      <Route exact component={accountLoadables.LoadableRegistration} path="/registration" />
      <Route
        exact
        component={accountLoadables.LoadableAccount}
        path="/account"
      />
      <Route
        exact
        component={accountLoadables.LoadableRoleFunctionList}
        path="/roleFunction"
      />
      <Route
        exact
        component={BLLoadables.LoadableLawyersList}
        path="/lawyers"
      />
      <Route exact component={BLLoadables.LoadableOrdersList} path="/orders" />
      <Route exact component={BLLoadables.LoadableOrder} path="/order" />

      <Route exact component={chatLoadables.LoadableChatTest} path="/chat/:orderId?" />
      <Route exact component={NotFound} path="*" />
    </Switch>
  );
};
export default Routes;
