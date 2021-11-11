import React from 'react';
import { Switch } from 'react-router-dom';

import Route from './Route';

import Simulation from '../pages/Simulation';
import Result from '../pages/Result';
import SignIn from '../pages/SignIn';
import SignUp from '../pages/SignUp';
import Dashboard from '../pages/Dashboard';
import AddCost from '../pages/AddCost';
import EditCost from '../pages/EditCost';
import EditGeneratorCost from '../pages/EditGeneratorCost';
import AddGeneratorCost from '../pages/AddGeneratorCost';

const routes: React.FC = () => (
  <Switch>
    <Route path="/" exact component={Simulation} isPublic />
    <Route path="/signin" component={SignIn} isPublic />
    <Route path="/signup" component={SignUp} isPublic />

    <Route path="/dashboard" component={Dashboard} isPrivate />
    <Route path="/addcost" component={AddCost} isPrivate />
    <Route path="/addgeneratorcost" component={AddGeneratorCost} isPrivate />
    <Route path="/editcost/:id" component={EditCost} isPrivate />
    <Route
      path="/editgeneratorcost/:id"
      component={EditGeneratorCost}
      isPrivate
    />
    <Route path="/result/:id" component={Result} isPublic />
  </Switch>
);

export default routes;
