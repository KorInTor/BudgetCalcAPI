--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

-- Started on 2025-01-21 18:29:47

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5 (class 2615 OID 92754)
-- Name: public; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA public;


--
-- TOC entry 4791 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 215 (class 1259 OID 92755)
-- Name: transaction; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.transaction (
    id bigint NOT NULL,
    category_id integer,
    user_id integer NOT NULL,
    created_at timestamp with time zone DEFAULT now() NOT NULL,
    amount money NOT NULL
);


--
-- TOC entry 216 (class 1259 OID 92759)
-- Name: Transaction_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public."Transaction_id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4792 (class 0 OID 0)
-- Dependencies: 216
-- Name: Transaction_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public."Transaction_id_seq" OWNED BY public.transaction.id;


--
-- TOC entry 217 (class 1259 OID 92760)
-- Name: transaction_category; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.transaction_category (
    id integer NOT NULL,
    name character varying NOT NULL,
    type_id integer
);


--
-- TOC entry 218 (class 1259 OID 92765)
-- Name: transaction_category_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.transaction_category_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4793 (class 0 OID 0)
-- Dependencies: 218
-- Name: transaction_category_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.transaction_category_id_seq OWNED BY public.transaction_category.id;


--
-- TOC entry 219 (class 1259 OID 92766)
-- Name: transaction_type; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.transaction_type (
    id integer NOT NULL,
    name character varying NOT NULL
);


--
-- TOC entry 220 (class 1259 OID 92771)
-- Name: transaction_type_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.transaction_type_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4794 (class 0 OID 0)
-- Dependencies: 220
-- Name: transaction_type_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.transaction_type_id_seq OWNED BY public.transaction_type.id;


--
-- TOC entry 221 (class 1259 OID 92772)
-- Name: user; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."user" (
    id integer NOT NULL,
    family_group integer,
    name character varying NOT NULL
);


--
-- TOC entry 222 (class 1259 OID 92777)
-- Name: user_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 4795 (class 0 OID 0)
-- Dependencies: 222
-- Name: user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.user_id_seq OWNED BY public."user".id;


--
-- TOC entry 4619 (class 2604 OID 92778)
-- Name: transaction id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction ALTER COLUMN id SET DEFAULT nextval('public."Transaction_id_seq"'::regclass);


--
-- TOC entry 4621 (class 2604 OID 92779)
-- Name: transaction_category id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_category ALTER COLUMN id SET DEFAULT nextval('public.transaction_category_id_seq'::regclass);


--
-- TOC entry 4622 (class 2604 OID 92780)
-- Name: transaction_type id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_type ALTER COLUMN id SET DEFAULT nextval('public.transaction_type_id_seq'::regclass);


--
-- TOC entry 4623 (class 2604 OID 92781)
-- Name: user id; Type: DEFAULT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user" ALTER COLUMN id SET DEFAULT nextval('public.user_id_seq'::regclass);


--
-- TOC entry 4778 (class 0 OID 92755)
-- Dependencies: 215
-- Data for Name: transaction; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.transaction VALUES (1, 1, 1, '2025-01-21 00:19:39.588024+07', '1 000,00 ?');
INSERT INTO public.transaction VALUES (2, 2, 2, '2025-01-21 00:19:52.328906+07', '-100,00 ?');


--
-- TOC entry 4780 (class 0 OID 92760)
-- Dependencies: 217
-- Data for Name: transaction_category; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.transaction_category VALUES (1, 'Зарплата', 2);
INSERT INTO public.transaction_category VALUES (2, 'Квартплата', 1);


--
-- TOC entry 4782 (class 0 OID 92766)
-- Dependencies: 219
-- Data for Name: transaction_type; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.transaction_type VALUES (2, 'Доход');
INSERT INTO public.transaction_type VALUES (1, 'Приход');


--
-- TOC entry 4784 (class 0 OID 92772)
-- Dependencies: 221
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."user" VALUES (1, 2, 'Андрей Асафьев');
INSERT INTO public."user" VALUES (2, 2, 'Стас Асафьев');
INSERT INTO public."user" VALUES (3, 1, 'Антон Антонов');
INSERT INTO public."user" VALUES (4, 1, 'Антоншка');
INSERT INTO public."user" VALUES (5, 1, 'Мария Антонова');


--
-- TOC entry 4796 (class 0 OID 0)
-- Dependencies: 216
-- Name: Transaction_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public."Transaction_id_seq"', 2, true);


--
-- TOC entry 4797 (class 0 OID 0)
-- Dependencies: 218
-- Name: transaction_category_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.transaction_category_id_seq', 2, true);


--
-- TOC entry 4798 (class 0 OID 0)
-- Dependencies: 220
-- Name: transaction_type_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.transaction_type_id_seq', 2, true);


--
-- TOC entry 4799 (class 0 OID 0)
-- Dependencies: 222
-- Name: user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.user_id_seq', 5, true);


--
-- TOC entry 4627 (class 2606 OID 92783)
-- Name: transaction_category transaction_category_pk; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_category
    ADD CONSTRAINT transaction_category_pk PRIMARY KEY (id);


--
-- TOC entry 4625 (class 2606 OID 92785)
-- Name: transaction transaction_pk; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction
    ADD CONSTRAINT transaction_pk PRIMARY KEY (id);


--
-- TOC entry 4629 (class 2606 OID 92787)
-- Name: transaction_type transaction_type_pk; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_type
    ADD CONSTRAINT transaction_type_pk PRIMARY KEY (id);


--
-- TOC entry 4631 (class 2606 OID 92789)
-- Name: user user_pk; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pk PRIMARY KEY (id);


--
-- TOC entry 4634 (class 2606 OID 92805)
-- Name: transaction_category transaction_category_transaction_type_fk; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction_category
    ADD CONSTRAINT transaction_category_transaction_type_fk FOREIGN KEY (type_id) REFERENCES public.transaction_type(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 4632 (class 2606 OID 92810)
-- Name: transaction transaction_transaction_category_fk; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction
    ADD CONSTRAINT transaction_transaction_category_fk FOREIGN KEY (category_id) REFERENCES public.transaction_category(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 4633 (class 2606 OID 92800)
-- Name: transaction transaction_user_fk; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.transaction
    ADD CONSTRAINT transaction_user_fk FOREIGN KEY (user_id) REFERENCES public."user"(id) ON UPDATE CASCADE ON DELETE CASCADE;


-- Completed on 2025-01-21 18:29:48

--
-- PostgreSQL database dump complete
--

