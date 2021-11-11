import React, { useEffect, useState } from 'react';

import { Link, useParams } from 'react-router-dom';

import api from '../../services/api';

import { Header, HeaderContent, Profile, Container, Content } from './styles';

interface Params {
  id: string;
}

interface Simulation {
  total: number;
  months: number;
}

const Result: React.FC = () => {
  const { id } = useParams<Params>();
  const [simulation, setSimulation] = useState<Simulation>();

  useEffect(() => {
    api.get(`v1/simulations/${id}`).then(response => {
      setSimulation(response.data);
    });
  }, [id]);

  return (
    <Container>
      <Header>
        <HeaderContent>
          <Profile>
            <div>
              <Link to="/dashboard">
                <strong>Aministrador</strong>
              </Link>
            </div>
          </Profile>
        </HeaderContent>
      </Header>
      <Content>
        <h1>Resultado da Simulação</h1>
        <h2>
          Custo aproximado do Gerador de Energia Solar: R$ {simulation?.total}
        </h2>
        <h2>
          Tempo em meses para ter o retorno do investimento:
          {simulation?.months} meses
        </h2>
      </Content>
    </Container>
  );
};

export default Result;
