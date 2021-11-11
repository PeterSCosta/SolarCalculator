import React, { useCallback, useEffect, useRef, useState } from 'react';
import { FormHandles } from '@unform/core';
import { Form } from '@unform/web';
import * as Yup from 'yup';
import { useParams } from 'react-router-dom';

import { FiPower, FiEdit } from 'react-icons/fi';
import { useHistory } from 'react-router-dom';
import getValidationError from '../../utils/getValidationErrors';

import { useToast } from '../../hooks/toast';
import api from '../../services/api';
import { useAuth } from '../../hooks/auth';

import {
  Container,
  Header,
  HeaderContent,
  Profile,
  Content,
  FormContent,
  ContentColumn,
} from './styles';

import Button from '../../components/Button';
import Input from '../../components/Input';

interface GeneratorCost {
  id: string;
  cost: number;
}

interface EditGeneratorCostFormData {
  cost: number;
}

interface Params {
  id: string;
}

const EditGeneratorCost: React.FC = () => {
  const { user, signOut } = useAuth();
  const formRef = useRef<FormHandles>(null);
  const history = useHistory();
  const { addToast } = useToast();
  const { id } = useParams<Params>();

  const [editingCost, setEditingCost] = useState<GeneratorCost>();

  useEffect(() => {
    api.get(`v1/generatorcosts/${id}`).then(response => {
      setEditingCost(response.data);
    });
  }, [id]);

  const handleSubmit = useCallback(
    async (data: EditGeneratorCostFormData) => {
      try {
        formRef.current?.setErrors({});
        const schema = Yup.object().shape({
          cost: Yup.number()
            .required('Custo obrigatória')
            .moreThan(0.1, 'Custo deve ser maior que 0.1'),
        });

        await schema.validate(data, {
          abortEarly: false,
        });

        await api.put(`/v1/generatorcosts/${id}`, data);

        history.push('/dashboard');
      } catch (err) {
        if (err instanceof Yup.ValidationError) {
          const errors = getValidationError(err);

          formRef.current?.setErrors(errors);

          return;
        }

        addToast({
          type: 'error',
          title: 'Erro no cadastro',
          description: 'Ocorreu um erro ao cadastrar custo',
        });
      }
    },
    [addToast, history, id],
  );

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
        <h1>Editar custo do gerador</h1>
        <Form ref={formRef} onSubmit={handleSubmit}>
          <FormContent>
            <ContentColumn>
              <Input
                name="cost"
                icon={FiEdit}
                placeholder="Custo KW/h"
                defaultValue={editingCost?.cost}
              />
            </ContentColumn>
          </FormContent>
          <Button type="submit">Editar</Button>
        </Form>
      </Content>
    </Container>
  );
};

export default EditGeneratorCost;
