import React, { useCallback, useRef } from 'react';
import { FiMail, FiUser, FiPhone } from 'react-icons/fi';

import { FormHandles } from '@unform/core';
import { Form } from '@unform/web';
import * as Yup from 'yup';
import { Link, useHistory } from 'react-router-dom';
import getValidationError from '../../utils/getValidationErrors';

import api from '../../services/api';

import { useToast } from '../../hooks/toast';

import Input from '../../components/Input';
import Button from '../../components/Button';

import {
  Header,
  HeaderContent,
  Profile,
  Container,
  Content,
  FormContent,
  ContentColumn,
} from './styles';

interface SimulationFormData {
  name: string;
  email: string;
  phone: string;
  state: string;
  city: string;
  zipcode: string;
  neighborhood: string;
  street: string;
  number: number;
  complement: string;
  energycostmonthly: number;
}

const SignUp: React.FC = () => {
  const formRef = useRef<FormHandles>(null);
  const { addToast } = useToast();
  const history = useHistory();

  const handleSubmit = useCallback(
    async (data: SimulationFormData) => {
      try {
        formRef.current?.setErrors({});
        const schema = Yup.object().shape({
          name: Yup.string().required('Nome obrigatório'),
          email: Yup.string()
            .required('E-mail obrigatório')
            .email('Digite um e-mail válido'),
          password: Yup.string().min(6, 'Minimo de 6 caracteres'),
        });

        await schema.validate(data, {
          abortEarly: false,
        });

        const response = await api.post('/v1/simulations', data);

        const { id } = response.data;

        history.push(`/result/${id}`);
      } catch (err) {
        if (err instanceof Yup.ValidationError) {
          const errors = getValidationError(err);

          formRef.current?.setErrors(errors);

          return;
        }

        addToast({
          title: 'Erro no cadastro',
          description: 'Ocorreu um erro ao fazer a simulação, tente novamente',
        });
      }
    },
    [addToast, history],
  );

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
        <h1>Faça sua simulação</h1>
        <Form ref={formRef} onSubmit={handleSubmit}>
          <FormContent>
            <ContentColumn>
              <Input name="name" icon={FiUser} placeholder="Nome" />
              <Input name="email" icon={FiMail} placeholder="E-mail" />
              <Input name="phone" icon={FiPhone} placeholder="Telefone" />
              <Input name="state" icon={FiMail} placeholder="Estado" />
              <Input name="city" icon={FiMail} placeholder="Cidade" />
              <Input name="zipcode" icon={FiMail} placeholder="CEP" />
              <Input name="neighborhood" icon={FiMail} placeholder="Bairro" />
              <Input name="street" icon={FiMail} placeholder="Rua" />
              <Input name="number" icon={FiMail} placeholder="Número" />
              <Input
                name="complement"
                icon={FiMail}
                placeholder="Complemento"
              />
              <Input
                name="energycostmonthly"
                icon={FiMail}
                placeholder="Valor médio conta de energia"
              />
            </ContentColumn>
          </FormContent>
          <Button type="submit">Simular</Button>
        </Form>
      </Content>
    </Container>
  );
};

export default SignUp;
