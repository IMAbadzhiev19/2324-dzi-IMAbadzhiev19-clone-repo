--This is sample data, which could be used when presenting so there is no time wasted to add data during the project showcase.

INSERT INTO dbo.[AspNetUsers]
VALUES ('0fd42317-ea56-4195-9898-87f9f3cd5231', '������', '��������', '', 0, NULL, 'ivo147', 'IVO147', 'ivo147@abv.bg', 'IVO147@ABV.BG', 0, 'AQAAAAIAAYagAAAAEN8IBLnY/cpUYmZp9xoN9V1SgIGShxklU3l+US3fWzpa5YW5yDJsuCSzKW92chmM4A==', 'E5RFXBC46LIDTF42L6CNY2JEPZS2J63C', '67608df4-29a0-4bb0-a89d-0efcbc2caff7', '0982561867', 0, 0, NULL, 1, 0)

INSERT INTO dbo.[AspNetRoles]
VALUES ('911ab320-e0ba-4a71-9127-a4e789e6ba7e', 'BaseUser', 'BASEUSER', NULL)

INSERT INTO dbo.[AspNetUserRoles]
VALUES ('0fd42317-ea56-4195-9898-87f9f3cd5231', '911ab320-e0ba-4a71-9127-a4e789e6ba7e')

--Depots

INSERT INTO dbo.[Depots]
VALUES ('0165b7b8-f094-4dc3-86cc-9f3ca1fd0451', '��_�����_1', '0fd42317-ea56-4195-9898-87f9f3cd5231', 1, '��������', '������', '��������', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 18:30:25.6033647', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 18:30:25.6033647')

INSERT INTO dbo.[AspNetRoles]
VALUES ('17ba754a-66f4-410d-b174-429399f290b3', 'DepotManager', 'DEPOTMANAGER', NULL)

INSERT INTO dbo.[AspNetUserRoles]
VALUES ('0fd42317-ea56-4195-9898-87f9f3cd5231', '17ba754a-66f4-410d-b174-429399f290b3')

INSERT INTO dbo.[Depots]
VALUES ('d1429008-2ba8-4626-abfb-0bd947e97cb2', '��_�����_2', '0fd42317-ea56-4195-9898-87f9f3cd5231', 25, '����������', '������', '��������', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 18:32:25.5033647', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 18:32:25.6033647')

INSERT INTO dbo.[Depots]
VALUES ('d5aa26af-0f3c-47d2-b401-c5bd1615581b', '��_�����_3', '0fd42317-ea56-4195-9898-87f9f3cd5231', 3, '���������� ������', '�����', '��������', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 18:33:25.6133647', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 18:33:25.6033647')

INSERT INTO dbo.[Depots]
VALUES ('407bcbf5-878a-4e57-8938-9dbfc6cad554', '��_�����_4', '0fd42317-ea56-4195-9898-87f9f3cd5231', 18, '��������', '������', '��������', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 18:33:25.6133647', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 18:33:25.6033647')

INSERT INTO dbo.[Depots]
VALUES ('ff56b72f-3e3d-4b4c-9e81-edcdb3844ba8', '��_�����_5', '0fd42317-ea56-4195-9898-87f9f3cd5231', 12, '��� �����', '�����', '��������', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 18:33:25.6133647', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 18:33:25.6033647')

--Pharmacies

INSERT INTO dbo.[Pharmacies]
VALUES ('cab8e190-f73b-42e1-88fc-d3f64217e70e', '��_������_1', '������������ ������', 24, 'Stefan Stambolov', 'Burgas', 'Bulgaria', '0fd42317-ea56-4195-9898-87f9f3cd5231', 'd5aa26af-0f3c-47d2-b401-c5bd1615581b', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 11:55:24.4033178', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 11:55:24.4033178')

INSERT INTO dbo.[AspNetRoles]
VALUES ('e7ac04e9-f9cc-4df5-919f-74271e6f7a24', 'Founder', 'FOUNDER', NULL)

INSERT INTO dbo.[AspNetUserRoles]
VALUES ('0fd42317-ea56-4195-9898-87f9f3cd5231', 'e7ac04e9-f9cc-4df5-919f-74271e6f7a24')

INSERT INTO dbo.[Pharmacies]
VALUES ('ae2ddf1e-51c1-452a-9da8-b66584576d76', '��_������_2', '������������ ������', 3, '��������', '�����', '��������', '0fd42317-ea56-4195-9898-87f9f3cd5231', NULL, '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 11:55:24.4033178', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 11:55:24.4033178')

INSERT INTO dbo.[Pharmacies]
VALUES ('fef3ad36-db84-44d8-b277-6cc0672a8f2b', '��_������_3', '������������ ������', 5, '��������������', '������', '��������', '0fd42317-ea56-4195-9898-87f9f3cd5231', NULL, '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 11:55:24.4033178', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 11:55:24.4033178')

INSERT INTO dbo.[Pharmacies]
VALUES ('2bd07df0-1d88-4e49-90f2-74421c5bd9c2', '��_������_4', '������������ ������', 23, '�������', '�����', '��������', '0fd42317-ea56-4195-9898-87f9f3cd5231', NULL, '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 11:55:24.4033178', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 11:55:24.4033178')

INSERT INTO dbo.[Pharmacies]
VALUES ('60a692cb-5885-48ba-afd4-2e85b36a6b94', '��_������_5', '������������ ������', 3, '�������', '�����', '��������', '0fd42317-ea56-4195-9898-87f9f3cd5231', NULL, '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 11:55:24.4033178', '0fd42317-ea56-4195-9898-87f9f3cd5231', '2024-04-07 11:55:24.4033178')