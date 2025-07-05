import pygame
import random
from settings import *

# Inicializar Pygame
pygame.init()

# Criar a tela
screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
pygame.display.set_caption("Jogo de Corrida Simples")

# Cores
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)

# Carregar imagens
car_img = pygame.image.load('assets/images/car.png')
obstacle_img = pygame.image.load('assets/images/obstacle.png')
background_img = pygame.image.load('assets/images/background.png')

# Carregar sons
collision_sound = pygame.mixer.Sound('assets/sounds/collision.wav')
background_music = pygame.mixer.music.load('assets/sounds/background_music.mp3')

# Função principal do jogo
def game_loop():
    car_x = SCREEN_WIDTH // 2 - CAR_WIDTH // 2
    car_y = SCREEN_HEIGHT - CAR_HEIGHT - 10
    speed = CAR_SPEED
    obstacle_speed = OBSTACLE_SPEED
    obstacles = []
    score = 0

    clock = pygame.time.Clock()
    pygame.mixer.music.play(-1)  # Tocar música de fundo (loop infinito)

    running = True
    while running:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                running = False

        # Movimentação do carro
        keys = pygame.key.get_pressed()
        if keys[pygame.K_LEFT] and car_x > 0:
            car_x -= speed
        if keys[pygame.K_RIGHT] and car_x < SCREEN_WIDTH - CAR_WIDTH:
            car_x += speed

        # Gerar novos obstáculos
        if random.randint(1, 60) == 1:
            obstacles.append([random.randint(0, SCREEN_WIDTH - OBSTACLE_WIDTH), -OBSTACLE_HEIGHT])

        # Mover obstáculos
        obstacles = [[x, y + obstacle_speed] for x, y in obstacles if y < SCREEN_HEIGHT]

        # Verificar colisões
        for obstacle in obstacles:
            if car_x < obstacle[0] + OBSTACLE_WIDTH and car_x + CAR_WIDTH > obstacle[0] and car_y < obstacle[1] + OBSTACLE_HEIGHT and car_y + CAR_HEIGHT > obstacle[1]:
                collision_sound.play()  # Tocar som de colisão
                print("Game Over! Pontuação final:", score)
                running = False

        # Atualizando a tela
        screen.fill(WHITE)
        screen.blit(background_img, (0, 0))  # Colocar o fundo
        screen.blit(car_img, (car_x, car_y))  # Desenhar o carro

        # Desenhar obstáculos
        for obstacle in obstacles:
            screen.blit(obstacle_img, (obstacle[0], obstacle[1]))

        # Atualizar a tela
        pygame.display.update()
        clock.tick(60)

# Rodar o jogo
game_loop()
pygame.quit()