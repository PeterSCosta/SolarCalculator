import React, { useCallback, useEffect, useState } from 'react';

import { FiPower, FiEdit } from 'react-icons/fi';
import { useHistory } from 'react-router-dom';

import {
  Container,
  Header,
  HeaderContent,
  Profile,
  Content,
  Table,
} from './styles';

import { useAuth } from '../../hooks/auth';
import api from '../../services/api';
import Button from '../../components/Button';

interface Cost {
  id: string;
  cost: number;
  state: string;
}

interface GeneratorCost {
  id: string;
  cost: number;
}

const Dashboard: React.FC = () => {
  const { signOut, user } = useAuth();
  const [costs, setCosts] = useState<Cost[]>([]);
  const [generatorCosts, setGeneratorCosts] = useState<GeneratorCost[]>([]);
  const history = useHistory();

  useEffect(() => {
    api.get(`/v1/costs`).then(response => {
      setCosts(response.data);
    });
  }, []);

  useEffect(() => {
    api.get(`/v1/generatorcosts`).then(response => {
      setGeneratorCosts(response.data);
    });
  }, []);

  const editCost = useCallback(
    (data: Cost) => {
      history.push(`/editcost/${data.id}`);
    },
    [history],
  );

  const addCost = useCallback(() => {
    history.push(`/addcost`);
  }, [history]);

  const editGeneratorCost = useCallback(
    (data: GeneratorCost) => {
      history.push(`/editgeneratorcost/${data.id}`);
    },
    [history],
  );

  const addGeneratorCost = useCallback(() => {
    history.push(`/addgeneratorcost`);
  }, [history]);

  return (
    <Container>
      <Header>
        <HeaderContent>
          <Profile>
            <div>
              <span>
                Bem-vindo,
                <strong>{user.userName}</strong>
              </span>
            </div>
          </Profile>
          <button type="button" onClick={signOut}>
            <FiPower />
          </button>
        </HeaderContent>
      </Header>
      <Content>
        <Table>
          <thead>
            <tr>
              <th>Estado</th>
              <th>Custo KW/h</th>
              <th>Editar</th>
            </tr>
          </thead>
          <tbody>
            {costs.map(cost => {
              return (
                <tr key={cost.id}>
                  <td>{cost.state}</td>
                  <td>{cost.cost}</td>
                  <td>
                    <button type="button" onClick={() => editCost(cost)}>
                      <FiEdit />
                    </button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </Table>
        <Button onClick={() => addCost()}>Adicionar Custo</Button>
        <Table>
          <thead>
            <tr>
              <th>Custo Gerador KW/h</th>
              <th>Editar</th>
            </tr>
          </thead>
          <tbody>
            {generatorCosts.map(generatorCost => {
              return (
                <tr key={generatorCost.id}>
                  <td>{generatorCost.cost}</td>
                  <td>
                    <button
                      type="button"
                      onClick={() => editGeneratorCost(generatorCost)}
                    >
                      <FiEdit />
                    </button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </Table>
        {generatorCosts.length === 0 && (
          <Button onClick={() => addGeneratorCost()}>
            Adicionar Custo Gerador
          </Button>
        )}
      </Content>
    </Container>
  );
};

export default Dashboard;
